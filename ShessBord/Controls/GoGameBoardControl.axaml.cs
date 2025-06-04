using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.VisualTree;
using DynamicData;
using ShessBord.Controls.GoBordControl;
using Brushes = Avalonia.Media.Brushes;
using Point = Avalonia.Point;

namespace ShessBord.Controls;


[TemplatePart("PART_GoGameBoardPresenter", typeof(GoGameBoardControl))]
public class GoGameBoardControl : TemplatedControl
{
    private static Canvas? _gridCanvas;
    private List<Control>? _controls;
    private UniformGrid? _uniformGrid;
    
    #region Changitem

    /// <summary> Defines the <see cref="IsLineSelection"/> propertty </summary>
    public static readonly StyledProperty<bool> IsChangeItemProperty =
        AvaloniaProperty.Register<GoGameBoardControl,
            bool>(nameof(IsChangeItem), defaultValue: true);

    /// <summary> Gets or sets the show line. </summary>
    public bool IsChangeItem
    {
        get { return GetValue(IsChangeItemProperty); }
        set { SetValue(IsChangeItemProperty, value); }
    }

    #endregion

    #region ChosenSell

    private static readonly DirectProperty<GoGameBoardControl, CellPlace> ChosenSellProperty =
        AvaloniaProperty.RegisterDirect<GoGameBoardControl, CellPlace>(
            nameof(ChosenSell),
            o => o.ChosenSell,
            (o, v) => o.ChosenSell = v);

    private CellPlace _chosenSell = new CellPlace(-1, -1);

    private CellPlace ChosenSell
    {
        get => _chosenSell;
        set
        {
            SetAndRaise(ChosenSellProperty, ref _chosenSell, value);
            if(IsLineSelection) InitializeCanvas();
        }
    }

    #endregion

    #region SelectedSideType

    public enum SelectedSideType
    {
        White,
        Black,
    }

    /// <summary> Defines the <see cref="SizeBoard"/> property. </summary>
    public static readonly StyledProperty<SelectedSideType> SelectedSideProperty =
        AvaloniaProperty.Register<GoGameBoardControl,
            SelectedSideType>(nameof(SizeBoard), defaultValue: SelectedSideType.Black);

    /// <summary> Gets or sets the Mini/Norm/Mox of columns and row. </summary>
    public SelectedSideType SelectedSide
    {
        get { return GetValue(SelectedSideProperty); }
        set { SetValue(SelectedSideProperty, value); }
    }

    #endregion

    #region BoardSize

    public enum BoardSize
    {
        Mini = 9,
        Norm = 13,
        Max = 19
    }

    /// <summary> Defines the <see cref="SizeBoard"/> property. </summary>
    public static readonly StyledProperty<BoardSize> SizeBoardProperty =
        AvaloniaProperty.Register<GoGameBoardControl,
            BoardSize>(nameof(SizeBoard), defaultValue: BoardSize.Norm);

    /// <summary> Gets or sets the Mini/Norm/Mox of columns. </summary>
    public BoardSize SizeBoard
    {
        get { return GetValue(SizeBoardProperty); }
        set { SetValue(SizeBoardProperty, value); }
    }

    #endregion

    #region StyleBoard

    /// <summary> Defines the <see cref="StyleBoard"/> property. </summary>
    public static readonly StyledProperty<string> StyleBoardProperty =
        AvaloniaProperty.Register<GoGameBoardControl,
            string>(nameof(StyleBoard), defaultValue: "Classic");

    /// <summary> Gets or sets the Style of columns. </summary>
    public string StyleBoard
    {
        get { return GetValue(StyleBoardProperty); }
        set { SetValue(StyleBoardProperty, value); }
    }

    #endregion

    #region IsLineSelection

    /// <summary> Defines the <see cref="IsLineSelection"/> propertty </summary>
    public static readonly StyledProperty<bool> IsLineSelectionProperty =
        AvaloniaProperty.Register<GoGameBoardControl,
            bool>(nameof(IsLineSelection), defaultValue: true);

    /// <summary> Gets or sets the show line. </summary>
    public bool IsLineSelection
    {
        get { return GetValue(IsLineSelectionProperty); }
        set { SetValue(IsLineSelectionProperty, value); }
    }

    #endregion

    #region Cells
    /// <summary> Defines the <see cref="Cells"/> property. </summary>
    public static readonly DirectProperty<GoGameBoardControl, ObservableCollection<CellBoard>> CellsProperty =
        AvaloniaProperty.RegisterDirect<GoGameBoardControl, ObservableCollection<CellBoard>>(
            nameof(Cells),
            o => o.Cells, (o, v) => o.Cells = v);

    private ObservableCollection<CellBoard> _cells = new();

    /// <summary> Gets or sets the cells of the board. </summary>
    public ObservableCollection<CellBoard> Cells
    {
        get { return _cells; }
        set { SetAndRaise(CellsProperty, ref _cells, value); }
    }

    #endregion

    #region IsConfirmationMode

    /// <summary> Defines the <see cref="IsConfirmationMode"/> propertty </summary>
    public static readonly StyledProperty<bool> IsConfirmationModeProperty =
        AvaloniaProperty.Register<GoGameBoardControl,
            bool>(nameof(IsConfirmationMode), defaultValue: true);

    /// <summary> Gets or sets the Confirmation Cells. </summary>
    public bool IsConfirmationMode
    {
        get { return GetValue(IsConfirmationModeProperty); }
        set { SetValue(IsConfirmationModeProperty, value); }
    }

    #endregion

    static GoGameBoardControl()
    {
        AffectsRender<GoGameBoardControl>(new AvaloniaProperty[]
        {
            StyleBoardProperty,
            SizeBoardProperty,
        });
    }

    public GoGameBoardControl()
    {
        InitializeCells();
    }

    // Inditialize Cells
    private void InitializeCells()
    {
        var sideLength = (int)SizeBoard;

        // Assignments Cells
        Cells = Cells.Count == sideLength * sideLength ? UpdateCells() : CreateCells(sideLength);
    }
    
    
    public static readonly StyledProperty<ICommand> MoveMadeCommandProperty =
        AvaloniaProperty.Register<GoGameBoardControl, ICommand>(nameof(MoveMadeCommand));

    public ICommand MoveMadeCommand
    {
        get => GetValue(MoveMadeCommandProperty);
        set => SetValue(MoveMadeCommandProperty, value);
    }

    // Create new Cells
    private ObservableCollection<CellBoard> CreateCells(int sideLength)
    {
        var countCells = sideLength * sideLength;
        var cells = new ObservableCollection<CellBoard>();
        
        _chosenSell = new CellPlace(-1, -1);

        for (var i = 0; i < countCells; i++)
        {
            var column = i % sideLength;
            var row = i / sideLength;

            // Create and add Cell
            cells.Add(new CellBoard(column, row, new RelayCommand(_ =>
            {
                var tempRow = row;
                var tempColumn = column;
                var tempCell = Cells.FirstOrDefault(x => x.Row == tempRow && x.Column == tempColumn);

                if (tempCell is not { Status: null })
                {
                    return;
                }
                
                ChosenSell = new CellPlace(tempColumn, tempRow);

                // Update this Cell
                Cells = UpdateCells(ChosenSell);
                
                // Вызываем команду с параметром хода
                if (MoveMadeCommand?.CanExecute(ChosenSell) == true)
                {
                    MoveMadeCommand.Execute(ChosenSell);
                }
            }),SelectedSide));
        }

        return cells;
    }

    // Update old Cells
    private ObservableCollection<CellBoard> UpdateCells(CellPlace cellPlace = null)
    {
        var cells = Cells;
        var newCells = new ObservableCollection<CellBoard>();

        if (cellPlace != null)
        {
            var status = SelectedSide == SelectedSideType.Black ? 'b' : 'w';
            // Update select sell
            cells.First(x => x.Column == cellPlace.Column && x.Row == cellPlace.Row).Status = status;

            foreach (var cell in cells)
            {
                cell.SideType = SelectedSide;
                newCells.Add(cell);
            }

            return newCells;
        }

        return cells;
    }

    // Add Columns and Rows to Grid
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        _uniformGrid = FindVisualChild<UniformGrid>(this.GetVisualChildren());

        if (_uniformGrid != null)
        {
            _uniformGrid.Columns = (int)SizeBoard;
            _uniformGrid.Rows = (int)SizeBoard;
        }
    }

    // Change Property Control (SizeBoard,Cells)
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        int countColumnsAndRow = (int)SizeBoard;

        if (change.Property.Name == nameof(SizeBoard)
            || change.Property.Name == nameof(Cells))
        {
            if (_uniformGrid != null)
            {
                _uniformGrid.Columns = countColumnsAndRow;
                _uniformGrid.Rows = countColumnsAndRow;
            }

            if (IsLineSelection)
            {
                InitializeCanvas();
            }

            InitializeCells();
        }

        if (change.Property.Name == nameof(IsChangeItem))
        {
            InitializeCells();
        }
        

        if (change.Property.Name == nameof(SizeBoard))
        {
            InitializeCanvas();
        }

        if (change.Property.Name == nameof(IsLineSelection))
        {
            if (IsLineSelection == false)
            {
                _chosenSell = new CellPlace(-1, -1);
            }

            InitializeCanvas();
        }

        if (change.Property.Name == nameof(SelectedSide))
        {
            InitializeCells();
        }
    }

    // Find Child Item
    private T? FindVisualChild<T>(IEnumerable<Visual> children, string? name = null)
    {
        foreach (var child in children)
        {
            if (child is T t
                && (name == null || name == child.Name))
            {
                return t;
            }

            var found = FindVisualChild<T>(child.GetVisualChildren(), name);

            if (found != null)
            {
                return found;
            }
        }

        return default;
    }

    // Load Canvas
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _gridCanvas = e.NameScope.Find<Canvas>("GridCanvas")!;
        
        InitializeCanvas();
    }
    
    // Draw Schema
    private List<Control> DrawSchema()
    {
        var sideCount = SizeBoard;
        var sideLength = Width;
        var sellWidth = sideLength / (int)sideCount;
        var halfSellWidth = sellWidth / 2;
        var listLines = new List<Control>();
        
        var temp = sideCount switch
        {
            BoardSize.Max => new [,] {{5,10},{3,3},{3,9},{9,3},{15,3},{3,15},{9,9},{15,15},{9,15},{15,9}},
            BoardSize.Norm => new [,] {{7,6},{3,3},{3,9},{9,3},{9,9},{6,6},},
            BoardSize.Mini => new [,] {{10,6},{2,2},{6,2},{4,4},{2,6},{6,6}},
            _ => throw new ArgumentOutOfRangeException()
        };
        
        for (var i = 1; i < temp[0,1]; i++)
        {
            var point = new Ellipse
            {
                Tag = "O",
                Fill = Brushes.Black,
                ZIndex = 0,
                Width = temp[0,0],
                Height = temp[0,0],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            
            Canvas.SetLeft(point, temp[i,0] * sellWidth + halfSellWidth - temp[0,0] / 2 );
            Canvas.SetTop(point, temp[i,1] * sellWidth + halfSellWidth - temp[0,0] / 2);
            
            listLines.Add(point);
        }
        
        for (var line = 0; line < (int)sideCount; line++)
        {
            var lineHorizontal = new Line
            {
                Tag = $"H" + line,
                StartPoint = new Point(halfSellWidth, (line * sellWidth) + halfSellWidth),
                EndPoint = new Point(sideLength - halfSellWidth, (line * sellWidth) + halfSellWidth ),
                Stroke = Brushes.Black,
                ZIndex = 1,
                StrokeThickness = (int)sideCount == 9 ? 1.3 : (int)sideCount == 13 ? 1.1 : 1
            };
            
            listLines.Add(lineHorizontal);
            
            var lineVertical = new Line
            {
                Tag = "V" + line,
                StartPoint = new Point( (line * sellWidth) + halfSellWidth, halfSellWidth),
                EndPoint = new Point((line * sellWidth) + halfSellWidth, sideLength - halfSellWidth),
                Stroke = Brushes.Black,
                ZIndex = 1,
                StrokeThickness = (int)sideCount == 9 ? 1.3 : (int)sideCount == 13 ? 1.1 : 1
            };
            
            listLines.Add(lineVertical);
        }

        return listLines;
    }
    
    // Select Place
    private List<Control> SelectPlace()
    {
        var listControls = _controls;

        if (ChosenSell.Column <= -1 || ChosenSell.Row <= -1)
        {
            return listControls!;
        }
        
        (listControls!.First(x => x.Tag!.ToString() == $"H{ChosenSell.Row}") as Line)!.Stroke = Brushes.Red;
        (listControls!.First(x => x.Tag!.ToString() == $"V{ChosenSell.Column}") as Line)!.Stroke = Brushes.Red;

        return listControls!;
    }
    
    // Initialize Canvas
    private void InitializeCanvas()
    {
        if (_gridCanvas == null) 
            return;
        
        // Draw
        if (true)
        {
            _controls = DrawSchema();
        }
        
        var listControls = _controls;

        if (ChosenSell is { Column: > -1, Row: > -1 })
        {
            listControls = SelectPlace();
        }
        
        _gridCanvas.Children.Clear();
        _gridCanvas.Children.Add(listControls);
    }
}
