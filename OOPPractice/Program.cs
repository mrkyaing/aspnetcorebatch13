using OOPPractice;

Engineer e = new Engineer();
e.SetName("Mg Mg ");
Car myCar=new Car();
myCar.Color = "RED";
myCar.Model = "2023Z";
myCar.LicneceNo = "YGN-2022";
myCar.PlayHorn();
myCar.PlayHorn(5);
Console.WriteLine(1);
Console.WriteLine("HGello");

Cat myCat = new Cat();
myCat.Color = "YELLOW";
myCat.Name = "SHWE WAH";
myCat.LifeSpan = 2;
myCat.Eat();
myCat.Speak();
myCat.ShowInfo();
Dog myDog = new Dog();
myDog.Color = "BLACK";
myDog.Name = "Jacky";
myDog.LifeSpan = 1;
myDog.Eat();
myDog.Speak();
myDog.ShowInfo();

Console.WriteLine("Practice for Abstraction demo");
SayHello sayHello = new MyanmarPeople();
sayHello.Name = "Mg Mg";
sayHello.Greeting();
sayHello.MySelf();

sayHello = new JapanesePeople();
sayHello.Name = "Koro Sann";
sayHello.Greeting();
sayHello.MySelf();

sayHello = new EnglishPeople();
sayHello.Name = "Smith";
sayHello.Greeting();
sayHello.MySelf();


