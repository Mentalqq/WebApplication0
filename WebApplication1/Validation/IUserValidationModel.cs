namespace WebApplication1.Validation
{
    public interface IUserValidationModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        int Age { get; set; }
    }
}
