pipelineJob('build-funapi-master') {
  definition {
    cpsScm {
      scm {
        git {
          remote {
            url('git@github.com:tim-vh/FunApi.git')
          }
          branch('*/master')
        }
      }
      scriptPath('jenkinsfile')
      lightweight()
    }
  }
}

pipelineJob('build-jenkins-master') {
  definition {
    cpsScm {
      scm {
        git {
          remote {
            url('git@github.com:tim-vh/FunApi.git')
          }
          branch('*/master')
        }
      }
      scriptPath('Jenkins/jenkinsfile')
      lightweight()
    }
  }
}