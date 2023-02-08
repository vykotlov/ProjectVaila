using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectVaila.Providers.EFCoreProvider.Data
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
