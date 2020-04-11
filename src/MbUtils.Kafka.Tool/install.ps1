dotnet tool uninstall -g MbUtils.Kafka.Tool
dotnet pack
dotnet tool install -g MbUtils.Kafka.Tool --add-source ./nupkg