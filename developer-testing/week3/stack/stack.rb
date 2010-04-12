class Stack2
  def initialize
    @items = []
  end

  def empty?
    @items.length == 0
  end

  def pop
    raise StackEmptyException.new unless @items.length > 0
    @items.pop
  end

  def top
    raise StackEmptyException.new unless @items.length > 0
    @items.last
  end

  def push(item)
    @items << item
  end

  def size
    @items.length
  end

end

class StackEmptyException < Exception
end
