using TimeProject.Application.ViewModels;

namespace TimeProject.Application.Interfaces
{
    public interface ITeamService
    {
        void Insert(TeamFormViewModel model);

        void Update(TeamFormViewModel model);

        TeamOptionsViewModel GetTeamOptions(string teamId = null);
    }
}
