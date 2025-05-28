using Application.Customer.Queries.GetCustomers;

namespace BookRental.DTOs.In.Customer;

public class GetCustomersRequest : IRequestIn<GetCustomersQuery>
{
    public GetCustomersQuery Convert()
    {
        return new GetCustomersQuery();
    }
}