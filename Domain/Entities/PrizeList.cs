namespace Domain.Entities;

public class PrizeList
{
    public List<Prize> Prizes = new List<Prize>()
    {
        new Prize() {Id = 1, Name = "Микроволновая печь", PrizeImage = "microwave.jpg", Cost = 500},
        new Prize() {Id = 2, Name = "Стиральная машина", PrizeImage = "washing_machine.jpeg", Cost = 800 },
        new Prize() {Id = 3, Name = "Мультиварка", PrizeImage = "multivarka.jpg", Cost = 400 },
    };
    public Prize MoneyPrize = new Prize() { Id = 0, Name = "Деньги", PrizeImage = "money.jpg" };
}
