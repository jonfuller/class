require 'sewing_machine'
require 'spec_helper'

describe SewingMachine do
  context '(when machine is initialized)' do
    before(:each) do
      @machine = SewingMachine.new(Size.new(10, 10))
    end

    it 'should raise invalid size if table dimension is zero or less' do
     lambda { SewingMachine.new(Size.new(-1, 1)) }.should raise_exception(InvalidSize) 
     lambda { SewingMachine.new(Size.new(1, 0)) }.should raise_exception(InvalidSize) 
    end

    it 'should report table size' do
      @machine.table_size.should be_of_size(Size.new(10, 10))
    end

    it 'should have empty workpiece size' do
      @machine.workpiece_size.should be_of_size(Size.empty)
    end

    it 'should be able to move to location within table size' do
      lambda { @machine.move_to(0, 0) }.should_not raise_exception(InvalidPosition)
      lambda { @machine.move_to(10, 10) }.should_not raise_exception(InvalidPosition)
    end

    it 'should not be able to move to negative location' do
      lambda { @machine.move_to(-1, 0) }.should raise_exception(InvalidPosition)
      lambda { @machine.move_to(0, -1) }.should raise_exception(InvalidPosition)
    end

    it 'should not be able to move to location outside of table size' do
      lambda { @machine.move_to(11, 0) }.should raise_exception(InvalidPosition)
      lambda { @machine.move_to(0, 11) }.should raise_exception(InvalidPosition)
    end

    it 'should raise no workpiece' do
      lambda { @machine.sew_to(1, 1) }.should raise_exception(NoWorkpiece)
    end
  end

  context '(when setting workpiece size)' do
    before(:each) do
      @machine = SewingMachine.new(Size.new(10, 10))
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
  end

  context '(when sewing)' do
     before(:each) do
       @machine = SewingMachine.new(Size.new(10, 10))
       @machine.workpiece_size = Size.new(6, 8)
     end

     it 'should raise invalid position when sewing anywhere after having moved to location outside of workpiece' do
       @machine.move_to(9, 9)
       lambda { @machine.sew_to(1, 1) }.should raise_exception(InvalidPosition)
     end

     it 'should sew to edge of workpiece' do
       @machine.sew_to(6, 8)
       @machine.current_location.should be_at(Point.new(6, 8))
     end

     it 'should raise invalid position when trying to sew outside of workpiece' do
       lambda { @machine.sew_to(7, 1) }.should raise_exception(InvalidPosition)
       lambda { @machine.sew_to(1, 9) }.should raise_exception(InvalidPosition)
     end

     it 'should raise invalid position when trying to sew to current location)' do
       @machine.move_to(0, 0)
       lambda { @machine.sew_to(0, 0) }.should raise_exception(InvalidPosition)
     end
  end
end
