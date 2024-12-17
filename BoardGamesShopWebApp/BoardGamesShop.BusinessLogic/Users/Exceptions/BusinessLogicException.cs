namespace BoardGamesShop.BusinessLogic.Users.Exceptions;

public class BusinessLogicException : Exception
{
    public ResultCode? ResultCode { get; init; }

    public BusinessLogicException()
    {
    }

    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(ResultCode resultCode) : base(resultCode.ToString())
    {
        ResultCode = resultCode;
    }
    
    public BusinessLogicException(ResultCode resultCode, string message) : base(resultCode.ToString() + message)
    {
        ResultCode = resultCode;
    }
}