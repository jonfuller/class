require 'stack'

describe Stack2 do
  context '(when stack is initialized and empty)' do
    before(:each) do
      @stack = Stack2.new
    end

    it 'should be empty' do
      @stack.should be_empty
    end

    it 'should have zero size' do
      @stack.size.should be(0)
    end

    it 'should throw stack empty exception when popped' do
      lambda { @stack.pop }.should raise_exception(StackEmptyException)
    end

    it 'should throw stack empty exception when peeking at top' do
      lambda { @stack.top }.should raise_exception(StackEmptyException)
    end

    it 'should push okay' do
      @stack.push 'hello'
      @stack.size.should be(1)
    end

    it 'should have pushed item on top' do
      @stack.push 'hello'
      @stack.top.should eql('hello')
    end

    it 'should no longer be empty after push' do
      @stack.push 'hello'
      @stack.should_not be_empty
    end
  end

  context '(when stack has 1 item)' do
    before(:each) do
      @stack = Stack2.new
      @stack.push 'hello'
    end

    it 'should return the item on top' do
      @stack.top.should eql('hello')
    end

    it 'should not be empty after top' do
      @stack.top
      @stack.should_not be_empty
    end

    it 'should return the item on pop' do
      @stack.pop.should eql('hello')
    end

    it 'should raise stack empty exception when popped twice' do
      lambda{
        @stack.pop
        @stack.pop }.should raise_exception(StackEmptyException)
    end

    it 'should be empty after single pop' do
      @stack.pop
      @stack.should be_empty
    end

    it 'should have 2 items after another push' do
      @stack.push 'goodbye'
      @stack.size.should be(2)
    end

    it 'should not be empty' do
      @stack.should_not be_empty
    end
  end

  context '(when stack has a bunch of items)' do
    before(:each) do
      @stack = Stack2.new
      200.times{ |i| @stack.push(i.to_s) }
    end

    it 'should return last pushed item on pop' do
      @stack.pop.should eql('199')
    end

    it 'should not be empty' do
      @stack.should_not be_empty
    end

    it 'should be able to push another item onto the stack' do
      @stack.push('another item')
      @stack.size.should be(201)
    end

    it 'should return last pushed item on top' do
      @stack.top.should eql('199')
    end
  end
end
