using InternetBanking.Application.Dto.User;
using InternetBanking.Application.ViewModels.User;

namespace InternetBanking.Presentation.Models
{
    public class AdminUsersViewModel
    {
        public required UsersInGroupsViewModel Users { get; set; }
        public required UserFilterDTO Filters { get; set; }
    }
}