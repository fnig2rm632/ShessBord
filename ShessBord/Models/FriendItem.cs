using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using ShessBord.DTO.Friend;

namespace ShessBord.Models;

public class FriendItem
{
    public FriendResponseDto Response { get; set; }
    public ReactiveCommand<string, Unit> RemoveCommand { get; set; }
    public bool IsVisibleRemove { get; set; }
    public ReactiveCommand<string, Unit> AcceptCommand { get; set; }
    public bool IsVisibleAccept { get; set; }
    public ReactiveCommand<string, Unit> SendCommand { get; set; }
    public bool IsVisibleSend { get; set; }
    public ReactiveCommand<string, Unit> PlayCommand { get; set; }
    
}