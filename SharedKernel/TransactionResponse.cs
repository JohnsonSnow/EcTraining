namespace SharedKernel;

public sealed record TransactionResponse(
    Guid transactionId, 
    string symbol, 
    string shortName, 
    string longName, 
    decimal bid, 
    decimal ask, 
    decimal regularMarketPrice, 
    decimal regularMarketDayHigh, 
    decimal regularMarketDayLow, 
    decimal regularMarketChange, 
    decimal regularMarketChangePercent, 
    decimal regularMarketOpen, 
    int quantity, 
    decimal pricePerShare, 
    DateTime purchaseDateAtUtcNow)
{
}
