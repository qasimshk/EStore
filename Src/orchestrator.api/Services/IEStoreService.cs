namespace orchestrator.api.Services;

using estore.common.Common.Results;
using estore.common.Models.Requests;
using estore.common.Models.Responses;

public interface IEStoreService
{
    Task<Result<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request);

    Task<Result<OrderResponse>> CreateOrder(CreateOrderRequest request);

    Task<Result<EmployeeResponse>> GetEmployeeById(int employeeId);

    Task<Result<CustomerResponse>> GetCustomerByPhoneNumber(string phoneNumber);

    Task<Result> DeleteCustomer(string customerId);

    Task<Result> DeleteOrder(int orderId);

    Task<IResult> SubmitOrder(SubmitOrderRequest submit);

    Task<IResult> GetOrderState(Guid correlationId);

    Task<IResult> RefundOrder(Guid correlationId);

    Task<IResult> RemoveOrder(Guid correlationId);

    Task<IResult> GetPaymentState(Guid correlationId);

    Task<IResult> GetAllOrders();

    Task<IResult> GetAllPayments();
}
