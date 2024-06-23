namespace Tools.Api.DTO;

public record SM4ECBInput(string Data, string Keys, bool EnOrDecrpyt, Base64OrHexEnum Type);

