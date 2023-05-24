namespace ExpenseTracker.Client.ViewModels
{
    public interface IProfileViewModel
    {
        public Task<bool> ChangePassword(string newPassword, string oldPassword);
        public Task<bool> ConfirmPassword(string newPassword);
    }
}
