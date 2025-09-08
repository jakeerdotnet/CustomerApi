using CustomerApi.Models;

namespace CustomerApi.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
}

public class CustomerService : ICustomerService
{
    private readonly List<Customer> _customers;
    private int _nextId = 51;

    public CustomerService()
    {
        _customers = GenerateSampleCustomers();
    }

    public Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return Task.FromResult<IEnumerable<Customer>>(_customers);
    }

    public Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(customer);
    }

    public Task<Customer> AddCustomerAsync(Customer customer)
    {
        customer.Id = _nextId++;
        _customers.Add(customer);
        return Task.FromResult(customer);
    }

    public Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
    {
        var existingCustomer = _customers.FirstOrDefault(c => c.Id == id);
        if (existingCustomer == null)
            return Task.FromResult<Customer?>(null);

        existingCustomer.Name = customer.Name;
        existingCustomer.Email = customer.Email;
        existingCustomer.Phone = customer.Phone;
        existingCustomer.City = customer.City;

        return Task.FromResult<Customer?>(existingCustomer);
    }

    public Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return Task.FromResult(false);

        _customers.Remove(customer);
        return Task.FromResult(true);
    }

    private static List<Customer> GenerateSampleCustomers()
    {
        var customers = new List<Customer>();
        var names = new[] 
        {
            "John Smith", "Emily Johnson", "Michael Brown", "Sarah Davis", "David Wilson",
            "Jessica Moore", "Christopher Taylor", "Amanda Anderson", "Daniel Thomas", "Lisa Jackson",
            "Matthew White", "Jennifer Harris", "Anthony Martin", "Michelle Thompson", "Mark Garcia",
            "Ashley Martinez", "Steven Robinson", "Stephanie Clark", "Joshua Rodriguez", "Rebecca Lewis",
            "Kevin Lee", "Nicole Walker", "Brian Hall", "Christina Allen", "Jason Young",
            "Elizabeth Hernandez", "Ryan King", "Samantha Wright", "Nicholas Lopez", "Lauren Hill",
            "Jacob Scott", "Rachel Green", "Tyler Adams", "Megan Baker", "Brandon Gonzalez",
            "Kayla Nelson", "Justin Carter", "Victoria Mitchell", "Aaron Perez", "Alexis Roberts",
            "Eric Turner", "Sydney Phillips", "Jonathan Campbell", "Jasmine Parker", "Samuel Evans",
            "Brittany Edwards", "Andrew Collins", "Danielle Stewart", "Joseph Sanchez", "Kimberly Morris"
        };

        var cities = new[]
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio",
            "San Diego", "Dallas", "San Jose", "Austin", "Jacksonville", "Fort Worth", "Columbus",
            "Charlotte", "San Francisco", "Indianapolis", "Seattle", "Denver", "Washington DC",
            "Boston", "El Paso", "Nashville", "Detroit", "Oklahoma City", "Portland", "Las Vegas",
            "Memphis", "Louisville", "Baltimore", "Milwaukee", "Albuquerque", "Tucson", "Fresno",
            "Sacramento", "Mesa", "Kansas City", "Atlanta", "Long Beach", "Colorado Springs",
            "Raleigh", "Miami", "Virginia Beach", "Omaha", "Oakland", "Minneapolis", "Tulsa",
            "Arlington", "Tampa", "New Orleans"
        };

        var domains = new[] { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "company.com" };

        for (int i = 1; i <= 50; i++)
        {
            var name = names[i - 1];
            var firstName = name.Split(' ')[0].ToLower();
            var lastName = name.Split(' ')[1].ToLower();
            var domain = domains[Random.Shared.Next(domains.Length)];
            var email = $"{firstName}.{lastName}@{domain}";
            
            var phone = $"({Random.Shared.Next(200, 999)}) {Random.Shared.Next(200, 999)}-{Random.Shared.Next(1000, 9999)}";
            var city = cities[Random.Shared.Next(cities.Length)];

            customers.Add(new Customer
            {
                Id = i,
                Name = name,
                Email = email,
                Phone = phone,
                City = city
            });
        }

        return customers;
    }
}