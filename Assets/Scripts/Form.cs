using System.Linq;

public class Form
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string City { get; set; }
	public string[] Hobbies { get; set; }
	public override string ToString()
	{
		return $"{FirstName} {LastName} from {City} with the following hobbies {Hobbies.Aggregate((acc,hob)=>acc+="\n"+hob)}";
	}
}