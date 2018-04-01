# Get the version from master branch
# by running gitversion.
# Make sure Gitversion is registered in PATH.

git checkout master
$version = gitversion /showvariable semver

# Update Directory Build Properties version in root.
# Version Property dictate all versions of all *.dlls.

$propsPath = ".\Directory.build.props"
[xml]$props = Get-Content $propsPath
$props.Project.PropertyGroup.Version = "$version"
$props.Save($propsPath)

# Commit Bump version
# And tag commit

git add $propsPath
git commit -m "Bump version for release"
git tag $version
git push origin $version --porcelain