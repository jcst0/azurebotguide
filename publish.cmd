nuget restore
msbuild QnABotAllFeatures.sln -p:DeployOnBuild=true -p:PublishProfile=.\yourroom-Web-Deploy.pubxml -p:Password=<PW>

