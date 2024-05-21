namespace Pharmacy.API.Mappers;

public class MappingException(string name, string value) : Exception($"unable to map {name} with value {value}")
{}
