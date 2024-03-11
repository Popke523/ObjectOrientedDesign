namespace ObjectOrientedDesign.FlightSystem.Object;

public class Crew : FlightSystemObject
{
    public Crew(ulong id, string name, ulong age, string phone, string email, ushort practice, string role)
    {
        Id = id;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
        Practice = practice;
        Role = role;
    }

    public ulong Id { get; set; }
    public string Name { get; set; }
    public ulong Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public ushort Practice { get; set; }
    public string Role { get; set; }
}