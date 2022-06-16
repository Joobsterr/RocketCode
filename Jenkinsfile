node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    def scannerHome = tool name: 'rocket-scanner';
    withSonarQubeEnv() {
      sh "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"RocketCOM\""
      sh "dotnet build"
      sh "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end"
    }
  }
}