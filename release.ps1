$version = gitversion /showvariable semver
[xml]$props = Get-Content .\Directory.build.props
$props.Project.PropertyGroup.Version = "$version"
$props.Save(".\Directory.build.props")
git add .
git commit -m "bump version"
git tag $version
git push