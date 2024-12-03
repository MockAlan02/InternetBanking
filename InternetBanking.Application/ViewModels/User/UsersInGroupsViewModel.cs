namespace InternetBanking.Application.ViewModels.User
{
    public class UsersInGroupsViewModel
    {
        public List<BaseUserViewModel> ActiveAdmins { get; set; } = new List<BaseUserViewModel>();
        public List<BaseUserViewModel> UnactiveAdmins { get; set; } = new List<BaseUserViewModel>();
        public List<BaseUserViewModel> ActiveCustomers { get; set; } = new List<BaseUserViewModel>();
        public List<BaseUserViewModel> UnactiveCustomers { get; set; } = new List<BaseUserViewModel>();
    }
}