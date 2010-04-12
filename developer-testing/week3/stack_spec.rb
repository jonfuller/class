require 'stack'

describe Stack2 do
  context '(when stack is initialized and empty)' do
    before(:each) do
      @stack = Stack2.new
    end

    it 'should be empty' do
      @stack.should be_empty
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

  end
end
