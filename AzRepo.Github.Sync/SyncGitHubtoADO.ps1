param(
     [Parameter()]
     [string]$GitHubSourcePAT,
 
     [Parameter()]
     [string]$ADODestinationPAT,
     
     [Parameter()]
     [string]$AzureRepoName,
     
     [Parameter()]
     [string]$ADOCloneURL,
     
     [Parameter()]
     [string]$GitHubCloneURL
 )

# Write your PowerShell commands here.
Write-Host ' - - - - - - - - - - - - - - - - - - - - - - - - -'
Write-Host ' reflect Azure Devops repo changes to GitHub repo'
Write-Host ' - - - - - - - - - - - - - - - - - - - - - - - - - '
#$AzureRepoName = "eSmartbench"
#$ADOCloneURL = "dev.azure.com/asahajit/Asahajit/_git/GitSyncTest"
#$GitHubCloneURL = "github.com/asahajit/GitSyncTest.git"
$stageDir = pwd | Split-Path
Write-Host "stage Dir is : $stageDir"
$AdoDir = $stageDir +"\"+"ado"
Write-Host "ADO Dir : $stageDir"
$destination = $AdoDir+"\"+ $AzureRepoName+".git"
Write-Host "destination: $destination"
#Please make sure, you remove https from azure-repo-clone-url
$sourceURL = "https://$($GitHubSourcePAT)"+"@"+"$($GitHubCloneURL)"
write-host "source URL : $sourceURL"
#Please make sure, you remove https from github-repo-clone-url
$destURL = "https://" + $($ADODestinationPAT) +"@"+"$($ADOCloneURL)"
write-host "dest URL : $destURL"
#Check if the parent directory exists and delete
if((Test-Path -path $AdoDir))
{
  write-host "source URL : $AdoDir"
  Remove-Item -Path $AdoDir -Recurse -force
}
if(!(Test-Path -path $AdoDir))
{
  New-Item -ItemType directory -Path $AdoDir
  Set-Location $AdoDir
  write-host "Desination path : $AdoDir"
  write-host "Desination url : $destURL"
  git clone --mirror $sourceURL
}
else
{
  Write-Host "The given folder path $destination already exists";
}
Set-Location $destination
Write-Output '*****Git removing remote secondary****'
git remote rm secondary
Write-Output '*****Git remote add****'
git remote add --mirror=fetch secondary $destURL
Write-Output '*****Git fetch origin****'
git fetch $sourceURL
Write-Output '*****Git push secondary****'
git push secondary  --all -f
Write-Output '**Azure Devops repo synced with Github repo**'
Set-Location $stageDir
if((Test-Path -path $AdoDir))
{
 Remove-Item -Path $AdoDir -Recurse -force
}
write-host "Job completed"