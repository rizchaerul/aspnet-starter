$EntityDir = "Entities";
$ContextName = "ApplicationDbContext";

$DbHost = "localhost";
$DbPort = "";
$DbName = "Application";
$UserName = "";
$Password = "";

# Npgsql.EntityFrameworkCore.PostgreSQL
# $ConnectionString = "Host=$($DbHost);Port=$($DbPort);Database=$($DbName);Username=$($UserName);Password=$($Password);";

# Microsoft.EntityFrameworkCore.SqlServer
# $ConnectionString = "Server=$($DbHost),$($DbPort);Database=$($DbName);User Id=$($UserName);Password=$($Password);TrustServerCertificate=True;";
$ConnectionString = "Server=$($DbHost);Database=$($DbName);Trusted_Connection=True;Encrypt=False;";

# Remove folder
Remove-Item -Recurse $EntityDir;

dotnet tool run dotnet-ef dbcontext scaffold $ConnectionString Microsoft.EntityFrameworkCore.SqlServer --context $ContextName --data-annotations --force --verbose --output-dir $EntityDir --no-onconfiguring;

# Remove default constructor
# See: https://github.com/dotnet/efcore/issues/12604
(Get-Content "./$($EntityDir)/$($ContextName).cs" -Raw) -replace "(?ms)$($ContextName)\(\).*?public ", "" | Set-Content "./$($EntityDir)/$($ContextName).cs"
