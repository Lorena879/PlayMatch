using CommunityToolkit.Mvvm.Input;
using PlayMatch.Models;

namespace PlayMatch.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}