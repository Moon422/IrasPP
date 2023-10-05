PROJECT=IrasPPBackend

run:
	dotnet run --project $(PROJECT)

dev:
	dotnet watch run --project $(PROJECT)
