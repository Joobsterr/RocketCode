pipeline {
  agent { 
    label 'built-in'
  }
  stages {
    stage('Build') {
      steps {
        script {
          def msbuild = tool name: 'RocketCOM', type: 'hudson.plugins.msbuild.MsBuildInstallation'
          bat "${msbuild} SimpleWindowsProject.sln"
        } 
      } 
    } 
  } 
} 