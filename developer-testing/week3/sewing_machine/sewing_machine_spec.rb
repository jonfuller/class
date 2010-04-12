require 'sewing_machine'
require 'spec_helper'

describe SewingMachine do
  context '(when machine is initialized)' do
    before(:each) do
      @machine = SewingMachine.new(Size.new(10, 10))
    end

    it 'should report table size' do
      @machine.table_size.should be_of_size(Size.new(10, 10))
    end

    it 'should have empty workpiece size' do
      @machine.workpiece_size.should be_of_size(Size.empty)
    end

    it 'should be able to set workpiece size to table size' do
      @machine.workpiece_size = Size.new(10, 10)
      @machine.workpiece_size.should be_of_size(@machine.table_size)
    end

    it 'should raise invalid size exception if workpiece is larger than table' do
      lambda { @machine.workpiece_size = Size.new(11, 1) }.should raise_exception(InvalidSize)
      lambda { @machine.workpiece_size = Size.new(1, 11) }.should raise_exception(InvalidSize)
    end

    it 'should raise invalid size if workpiece dimension is zero or less' do
      lambda { @machine.workpiece_size = Size.new(-1, 1) }.should raise_exception(InvalidSize)
      lambda { @machine.workpiece_size = Size.new(1, 0) }.should raise_exception(InvalidSize)
    end

    it 'should raise invalid size if table dimension is zero or less' do
     lambda { SewingMachine.new(Size.new(-1, 1)) }.should raise_exception(InvalidSize) 
     lambda { SewingMachine.new(Size.new(1, 0)) }.should raise_exception(InvalidSize) 
    end
  end
end
