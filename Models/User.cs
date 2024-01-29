namespace Models;

public enum UserRole
{
	ADMIN,
	REGULAR
}

public class User
{

	public User() { }

	public User(string username, string passwordHash, UserRole role)
	{
		Id = Guid.NewGuid();
		UserName = username;
		PasswordHash = passwordHash;
		Role = role;
	}

	public Guid? Id { get; set; }
	public string? UserName { get; set; }
	public string? PasswordHash { get; set; }

	public Cart? Cart { get; set; }
	public UserRole Role { get; set; }
}