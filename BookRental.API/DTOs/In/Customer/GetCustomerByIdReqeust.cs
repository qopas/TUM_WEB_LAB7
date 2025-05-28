using Application.Customer.Queries.GetCustomerById;

namespace BookRental.DTOs.In.Customer;

public class GetCustomerByIdRequest : IRequestIn<GetCustomerByIdQuery>
{
    public string Id { get; set; }

    public GetCustomerByIdQuery Convert()
    {
        return new GetCustomerByIdQuery
        {
            Id = Id
        };
    }
}