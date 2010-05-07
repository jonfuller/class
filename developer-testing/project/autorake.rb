watch(/\.cs$/) { |m| system 'rake' }
watch('^.*results.xml$') { |m| system 'rake growltest' }
