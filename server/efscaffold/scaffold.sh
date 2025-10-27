#!/bin/bash
set -a
source .env
set +a

if [ -z "$CONN_STR" ]; then
  echo "CONN_STR is not set. Check your .env file."
  exit 1
fi

dotnet tool install -g dotnet-ef
dotnet ef dbcontext scaffold "$CONN_STR" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --context-dir . \
  --output-dir ./Entities \
  --namespace efscaffold.Entities \
  --no-onconfiguring \
  --context-namespace Infrastructure.Postgres.Scaffolding \
  --context MyDbContext \
  --schema todosystem \
  --force