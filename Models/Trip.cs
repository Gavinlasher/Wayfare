using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wayfare.Models
{
  public class Trip
  {
    public string Name { get; set; }
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public Account Creator { get; set; }
  }
}