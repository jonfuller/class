class SewingMachine
  attr_accessor :table_size
  attr_accessor :workpiece_size
  attr_reader :current_location

  def initialize(table_size)
    verify_positive_value_size(table_size)
    @table_size = table_size
    @workpiece_size = Size.empty
    @current_location = Point.origin
  end

  def workpiece_size=(new_size)
    verify_positive_value_size(new_size)
    raise InvalidSize if (new_size.width > @table_size.width || new_size.height > @table_size.height)
    @workpiece_size = new_size
  end

  def move_to(x, y)
    raise InvalidPosition if x < 0 || y < 0
    raise InvalidPosition if x > @table_size.width
    raise InvalidPosition if y > @table_size.height
    @current_location = Point.new(x, y)
  end

  def sew_to(x, y)
    raise NoWorkpiece if @workpiece_size.width == 0
    raise InvalidPosition if @current_location.x > @workpiece_size.width
    raise InvalidPosition if @current_location.y > @workpiece_size.height
    
    raise InvalidPosition if x > @workpiece_size.width
    raise InvalidPosition if y > @workpiece_size.height

    raise InvalidPosition if x == @current_location.x && y == @current_location.y

    move_to(x, y)
  end

  def verify_positive_value_size(size)
    raise InvalidSize if (size.width <= 0 || size.height <= 0)
  end
end

class Point
  attr_reader :x
  attr_reader :y

  def Point.origin
    Point.new
  end

  def initialize(x=0, y=0)
    @x, @y = x, y
  end

  def to_s
    "[x='#{@x}', y='#{@y}']" end
end


class Size
  attr_reader :width
  attr_reader :height

  def Size.empty
    Size.new
  end

  def initialize(width=0, height=0)
    @height, @width = height, width
  end

  def to_s
    "[width='#{@width}', height='#{@height}']"
  end
end

class InvalidSize < Exception
end

class InvalidPosition < Exception
end

class NoWorkpiece < Exception
end
