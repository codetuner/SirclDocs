
To create new migrations:

PM> Add-Migration CreateSamplesSchema -Context SamplesDbContext -OutputDir Data\Samples\Migrations

To apply migrations:

PM> Update-Database -Context SamplesDbContext

