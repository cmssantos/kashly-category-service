dotnet ef migrations add InitialCreate \
    -p Kashly.Category.Infrastructure \
    -s Kashly.Category.Api \
    -o Data/Migrations \
    --context WriteDbContext
