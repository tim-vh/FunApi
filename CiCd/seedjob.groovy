pipelineJob('build-master') {
  definition {
    cpsScm {
      scm {
        git {
          remote {
            url('https://github.com/tim-vh/FunApi.git')
          }
          branch('*/master')
        }
      }
      scriptPath('CiCd/jenkinsfile')
      lightweight()
    }
  }
}