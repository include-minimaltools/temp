namespace BusinessLogic.Utils;

public class TokenSettings
{
  public string SecurityKey { get; set; } = string.Empty;
  public string AudienceToken { get; set; } = string.Empty;
  public string IssuerToken { get; set; } = string.Empty;
  public int ExpireTime { get; set; } = 120;
}