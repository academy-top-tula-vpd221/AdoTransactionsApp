using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=work_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open\n");

    SqlCommand command = connection.CreateCommand();

    SqlTransaction transaction = connection.BeginTransaction();
    command.Transaction = transaction;

    try
    {
        command.CommandText = "INSERT INTO products (title, price) VALUES ('Horse', 11000.00)";
        await command.ExecuteNonQueryAsync();
        command.CommandText = "INSERT INTO products (title, price) VALUES ('Piggy', 'hfghfghf')";
        await command.ExecuteNonQueryAsync();


        await transaction.CommitAsync();
        Console.WriteLine("Products adds to table");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error with adds products");
        await transaction.RollbackAsync();
    }
}