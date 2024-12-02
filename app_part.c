#include "postgres.h"
#include "fmgr.h"

PG_MODULE_MAGIC;

PG_FUNCTION_INFO_V1(add_part_c);

Datum
add_part_c(PG_FUNCTION_ARGS)
{
    char* name_part = PG_GETARG_CSTRING(0);
    char* part_number = PG_GETARG_CSTRING(1);
    char* status = PG_GETARG_CSTRING(2);
    int64 time_assigned = PG_GETARG_INT64(3);
    int64 assigned_to = PG_GETARG_INT64(4);
    int64 id_location = PG_GETARG_INT64(5);

    SPI_connect();

    char query[512];
    snprintf(query, sizeof(query),
        "INSERT INTO parts_table (name_part, part_number, status, time_assigned, assigned_to, id_location) "
        "VALUES ('%s', '%s', '%s', %ld, %ld, %ld);",
        name_part, part_number, status, time_assigned, assigned_to, id_location);

    SPI_exec(query, 0);

    SPI_finish();
    PG_RETURN_VOID();
}
