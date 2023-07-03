using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Entity;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Category(string name, string description, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        
        Validate();
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }
    
    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }
    
    private void Validate()
    {
        ValidateStringIsNullOrEmpty(Name, nameof(Name));
        ValidateStringLength(Name, nameof(Name), 3, 255);
        
        ValidateStringIsNull(Description, nameof(Description));
        ValidateStringLength(Description, nameof(Description), 0, 10000);
    }
    
    private void ValidateStringIsNullOrEmpty(string value, string name)
    {
        if (String.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{name} should not be empty or null.");
    }
    
    private void ValidateStringIsNull(string value, string name)
    {
        if (value == null)
            throw new EntityValidationException($"{name} should not be null.");
    }
    
    private void ValidateStringLength(string value, string name, int minLength, int maxLength)
    {
        if(value.Length < minLength)
            throw new EntityValidationException($"{name} should be at least {minLength} characters long.");
        
        if(value.Length > maxLength)
            throw new EntityValidationException($"{name} should be less or equal {maxLength} characters long.");
    }
}