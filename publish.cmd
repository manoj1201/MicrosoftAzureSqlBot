nuget restore
msbuild Microsoft.Bot.Sample.QnABot.sln -p:DeployOnBuild=true -p:PublishProfile=azuresqlbot-Web-Deploy.pubxml -p:Password=PdDi5TseBTf0vvBiAfEc6CAnA5cudav2S1o5KRdhKSEJ0u2WAW4nMgHe51Rn

