class SewingMachine
  attr_accessor :table_size

  def initialize(table_size)
    @table_size = table_size
  end
end

class Size
  attr_accessor :width
  attr_accessor :height

  def initialize(height=0, width=0)
    @height, @width = height, width
  end

  def to_s
    "[width='#{@width}', height='#{height}']"
  end
end
