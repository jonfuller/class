require 'sewing_machine'
require 'spec_helper'

describe SewingMachine do
  context '(when machine is initialized)' do
    before(:each) do
      @machine = SewingMachine.new(Size.new(10, 10))
    end

    it 'should report table size' do
      @machine.table_size.should be_of_size({:width=>10, :height=>10})
    end


      #lambda { @machine.whatever }.should raise_exception(WhateverException)
  end
end
