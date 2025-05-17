using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Interfaces.Identity;
using SK.Solution.Shared.Model.Crm;
using SK.Solution.Shared.Model.Identity;
using SK.Visit.UI.Blazor.Model;
using System.Collections.Generic;

namespace SK.Visit.UI.Blazor.Services
{
    public class VisitPlanningServices : IVisitPlanningServices
    {

        private readonly ISharedUserServices _sharedUserServices;
        private readonly ISharedCustomerServices _sharedCustomerServices;


        public VisitPlanningServices(ISharedUserServices sharedUserServices, ISharedCustomerServices sharedCustomerServices)
        {
            _sharedUserServices = sharedUserServices;
            _sharedCustomerServices = sharedCustomerServices;
        }
        private List<VisitPlanning> visitPlannings = new()
        {
            //new VisitPlanning { _Agent = new Agent { Id="1", Name = "John Doe", Color = "#5b43f2" }, Destinations = new List<Destination>() },
            //new VisitPlanning { _Agent = new Agent { Id="2", Name = "Jane Smith", Color = "#00b981" }, Destinations = new List<Destination>() },
            //new VisitPlanning { _Agent = new Agent { Id="3", Name = "khalil safraoui", Color = "#000000" }, Destinations = new List<Destination>() }
        };

        private List<Destination> Destinations = new()
        {
            //new Destination { Name = "David Wilson", Address = "202 Elm St, Newton, MA", Phone = "617-555-5678", Priority = "High", CustomerId= "1", OrderId="", Mark = new Mark(34.7406, 10.7603,"David Wilson","1"), CountryId ="TN", StateId="BN", CityId="1"},
            //new Destination { Name = "Emily Johnson", Address = "456 Park Ave, Cambridge, MA", Phone = "617-555-2345", Priority = "Medium", IsDelevery = true, CustomerId= "2", OrderId="1", Mark = new Mark(35.8256, 10.6084,"Emily Johnson","3"), CountryId ="TN", StateId="BN", CityId="1"},
            //new Destination { Name = "Jennifer Taylor", Address = "303 Cedar Ln, Watertown, MA", Phone = "617-555-6789", Priority = "Low", CustomerId= "3", OrderId="", Mark = new Mark(37.2744,9.8739,"Jennifer Taylor","2"), CountryId ="TN", StateId="BA", CityId="2"},
            //new Destination { Name = "John Smith", Address = "123 Main St, Boston, MA", Phone = "617-555-1234", Priority = "High", CustomerId= "4", OrderId="", Mark = new Mark(33.8818,10.0982,"John Smith","4") , CountryId ="TN", StateId="BA", CityId="3"},
            //new Destination { Name = "Lisa Anderson", Address = "505 Maple Ave, Malden, MA", Phone = "617-555-8901", Priority = "High", CustomerId= "5", OrderId="", Mark = new Mark(35.6781,10.0963,"Lisa Anderson","5"), CountryId ="TN", StateId="BA", CityId="4" },
            //new Destination { Name = "Michael Brown", Address = "789 Oak Rd, Somerville, MA", Phone = "617-555-3456", Priority = "Low", IsDelevery = true, CustomerId= "6", OrderId="2", CountryId ="TN", StateId="BZ", CityId="5" },
            //new Destination { Name = "Robert Martinez", Address = "404 Birch Dr, Medford, MA", Phone = "617-555-7890", Priority = "Medium", CustomerId= "7", OrderId="", CountryId ="TN", StateId="BZ", CityId="6" },
            //new Destination { Name = "Sarah Davis", Address = "101 Pine St, Brookline, MA", Phone = "617-555-4567", Priority = "Medium", IsDelevery = true, CustomerId= "8", OrderId="3" , CountryId ="TN", StateId="BB", CityId="7"},
        };

        private List<KeyValueAddress> Countries = new()
        {
             new KeyValueAddress { _key = "TN", _value = "Tunisia" },
             new KeyValueAddress { _key = "FR", _value = "France" },
             new KeyValueAddress { _key = "BL", _value = "Belgium" },
        };
        private List<KeyValueAddress> States = new()
        {
            new KeyValueAddress { _key = "BA", _value = "Bizerte" },
            new KeyValueAddress { _key = "BB", _value = "Bordj Bou Arreridj" },
            new KeyValueAddress { _key = "BZ", _value = "Beja" },
            new KeyValueAddress { _key = "BN", _value = "Ben Arous" },
            new KeyValueAddress { _key = "BS", _value = "Béjaïa" },
            new KeyValueAddress { _key = "NA", _value = "Namur" },

        };
        private List<KeyValueAddress> Cities = new()
        { 
            new KeyValueAddress { _key = "1", _value = "Tunis" },
            new KeyValueAddress { _key = "2", _value = "Sousse" },
            new KeyValueAddress { _key = "3", _value = "Sfax" },
            new KeyValueAddress { _key = "4", _value = "Kairouan" },
            new KeyValueAddress { _key = "5", _value = "Nabeul" },
            new KeyValueAddress { _key = "6", _value = "Gabès" },
            new KeyValueAddress { _key = "7", _value = "Monastir" },
            new KeyValueAddress { _key = "NA", _value = "Namur" },
        };

        public List<Destination> GetDestinationsBySelectedDate(VisitPlanning visitPlanning, DateTime selectedDate)
        {
            if (visitPlanning.Destinations.Count == 0)
            {
                return new List<Destination>() { new Destination { IsFake = true, Date = selectedDate } };
            }

            if (visitPlanning.Destinations.Where(v => v.Date == selectedDate).Count() == 0)
            {
                visitPlanning.Destinations.RemoveAll(i => i.IsFake == true && i.Date != selectedDate);
                visitPlanning.Destinations.Add(new Destination { IsFake = true, Date = selectedDate });
                return visitPlanning.Destinations.Where(v => v.Date == selectedDate).ToList();

            }
            return visitPlanning.Destinations.Where(v => v.Date == selectedDate).ToList();
        }

        public int DestinationCountBySelectedDate(VisitPlanning visitPlanning, DateTime selectedDate)
        {
            return visitPlanning.Destinations.Count(v => v.Date == selectedDate && v.IsFake == false);
        }

        public List<Destination> AddDestination(VisitPlanning visitPlanning, Destination destination, DateTime selectedDate)
        {
            VisitPlanning _visitPlanning = visitPlannings.First(i => i._Agent.Id == visitPlanning._Agent.Id);




            if (!_visitPlanning.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.Date == selectedDate))
            {
                if(visitPlannings.Exists(i => i.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.IsDelevery)))
                {
                   foreach(VisitPlanning visitPlanningToPerformeRemoe in visitPlannings.Where(i => i.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.IsDelevery)))
                    {
                        visitPlanningToPerformeRemoe.Destinations.RemoveAll(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.IsDelevery);
                    }
                }
                Destination newDestination  = destination.Clone;
                newDestination.Date = selectedDate;
                _visitPlanning.Destinations.Add(newDestination);

                Destinations.Where(i => i.CustomerId == destination.CustomerId && i.OrderId == destination.OrderId).ToList().ForEach(i => {
                    i.IsSelected = true;
                    i.TotalVisitPlanned = IncrementVisitPlannedCount(i);
                });

                // this is to remove the fake destination if it exists (its a workarround solution for a problem with blazor-draganddrop)
                if (_visitPlanning.Destinations.Where(x => x.Date == selectedDate).Count() > 1)
                {
                    _visitPlanning.Destinations.RemoveAll(i => i.IsFake == true && i.Date == selectedDate);
                }
            }
            return visitPlanning.Destinations;
        }

        private int IncrementVisitPlannedCount(Destination dist)
        {
            if (dist.TotalVisitPlanned == 0 || !dist.IsDelevery)
                dist.TotalVisitPlanned++;
            return dist.TotalVisitPlanned;
        }

        private int DecrementVisitPlannedCount(Destination dist)
        {
            if (dist.TotalVisitPlanned > 0)
                dist.TotalVisitPlanned--;
            return dist.TotalVisitPlanned;
        }
        private bool IsSelecedCheckBeforeDelete(Destination dist)
        {
            if (dist.TotalVisitPlanned == 1)
                return false;
            return true;
        }

        public async Task<List<VisitPlanning>> GetVisitPlanningsAsync()
        {
            List<UserDto> users = await _sharedUserServices.GetUsersInRolesAsync(new[] { "Admin", "Manager" });
            users.ForEach(user =>
            {
               visitPlannings.Add(new VisitPlanning { _Agent = new Agent { Id = user.UserId, Name = user.FirstName + " " + user.LastName, Color = GetRandomHexColor() }, Destinations = new List<Destination>() });
            });
            return visitPlannings;
        }

        public static string GetRandomHexColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}"; // Generates something like "#A1B2C3"
        }


        public async Task<List<Destination>> GetDistinations()
        {
            List<SharedCustomerDto> customers = await _sharedCustomerServices.GetCusomersAsync();
            customers.ForEach(customer =>
            {
                customer.Addresses.ToList().ForEach(address =>
                {
                   
                    Destinations.Add(new Destination { 
                        CustomerId = customer.Id.ToString(),
                        Name = customer.FirstName + " " + customer.LastName,
                        Address = customer.Address,
                        Phone = customer.PhoneNumber, 
                        Priority = "Low", 
                        CityId =address.CityId,
                        CountryId = address.CountryId,
                        StateId = address.StateId,
                        Mark = address.Latitude != decimal.Parse("0,000000") ? new Mark(double.Parse(address.Latitude.ToString()), double.Parse(address.Longitude.ToString()), customer.FirstName + " " + customer.LastName, customer.Id.ToString()) : null,
                        
                    });
                    
                });
               
            });
            return Destinations;
        }

        public List<KeyValueAddress> GetCountries()
        {
            return Countries;
        }

        public List<KeyValueAddress> GetStates()
        {
            return States;
        }

        public List<KeyValueAddress> GetCities()
        {
            return Cities;
        }

        public bool DeleteVisitPlanned(VisitPlanning visitPlanning, Destination destination, DateTime selectedDate)
        {
            VisitPlanning _visitPlanning = visitPlannings.First(i => i._Agent.Id == visitPlanning._Agent.Id);
            if (_visitPlanning.Destinations.Exists(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.Date == selectedDate))
            {
                _visitPlanning.Destinations.RemoveAll(i => i.OrderId == destination.OrderId && i.CustomerId == destination.CustomerId && i.Date == selectedDate);
                Destinations.Where(i => i.CustomerId == destination.CustomerId && i.OrderId == destination.OrderId).ToList().ForEach(i => {
                    i.IsSelected = IsSelecedCheckBeforeDelete(i);
                    i.TotalVisitPlanned = DecrementVisitPlannedCount(i);
                });
                return true;
            }
            return false;
        }
    }
}
