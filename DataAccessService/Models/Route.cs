using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessService.Models
{
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string RouteFirstStop { get; set; }
        public string RouteLastStop { get; set; }
        public double RouteDistanceKm { get; set; }
    }
}
