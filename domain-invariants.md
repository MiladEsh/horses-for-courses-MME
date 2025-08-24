# Horses For Courses
## Domain
Coach name cannot be an empty string.  
```csharp
Assert.Throws<CoachNameCanNotBeEmpty>(
    () => new Coach("", "mail@example.com"));
```
Coach email cannot be an empty string.  
```csharp
Assert.Throws<CoachEmailCanNotBeEmpty>(
    () => new Coach("Coachy", ""));
```
