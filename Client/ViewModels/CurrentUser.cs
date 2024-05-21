using System;

public class CurrentUser
{
    private static CurrentUser instance;
    private string? username;
    private string? email;

    private CurrentUser()
    {
        // Initialize username and email
        username = null;
        email = null;
    }

    public static CurrentUser GetInstance()
    {
        if (instance == null)
        {
            instance = new CurrentUser();
        }
        return instance;
    }

    public void SetUsername(string username)
    {
        // Set username
        this.username = username;
    }

    public string GetUsername()
    {
        return username;
    }

    public void SetEmail(string email)
    {
        // Set email
        this.email = email;
    }

    public string GetEmail()
    {
        return email;
    }
}
