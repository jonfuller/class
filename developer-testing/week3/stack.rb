class Stack2
  def init
  end

  def empty?()
    !@item
  end

  def pop
    raise StackEmptyException.new unless @item
    @item
  end

  def top
    raise StackEmptyException.new unless @item
    @item
  end

  def push(item)
    @item = item
  end

  def size
    1
  end

end

class StackEmptyException < Exception
end
