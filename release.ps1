# Get the version from master branch by running gitversion.
# Make sure Gitversion is registered in PATH!

$version = gitversion /showvariable semver

# Update Directory.build.props version <version></version>.
# It dictates all *.dll versions.

$propsPath = ".\Directory.build.props"
[xml]$props = Get-Content $propsPath
$props.Project.PropertyGroup.Version = "$version"
$props.Save($propsPath)

# Commit bump version and tag commit.
# Then push the newly tagged commit to origin.

git add $propsPath
git commit -m "Bump version for release"
git tag $version
git push --follow-tags --porcelain