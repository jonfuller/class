class SewingMachine
  attr_accessor :table_size
  attr_accessor :workpiece_size

  def initialize(table_size)
    verify_positive_value_size(table_size)
    @table_size = table_size
    @workpiece_size = Size.empty
  end

  def workpiece_size=(new_size)
    verify_positive_value_size(new_size)
    raise InvalidSize if (new_size.width > @table_size.width || new_size.height > @table_size.height)
    @workpiece_size = new_size
  end

  def verify_positive_value_size(size)
    raise InvalidSize if (size.width <= 0 || size.height <= 0)
  end
end

class Size
  attr_reader :width
  attr_reader :height

  def Size.empty
    Size.new
  end

  def initialize(height=0, width=0)
    @height, @width = height, width
  end

  def to_s
    "[width='#{@width}', height='#{@height}']"
  end
end

class InvalidSize < Exception
end
