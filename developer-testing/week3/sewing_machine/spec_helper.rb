Spec::Matchers.define :be_of_size do |size_expected|
  match do |size_actual|
    (size_actual.width == size_expected.width) && (size_actual.height == size_expected.height)
  end

  failure_message_for_should do |size_actual|
    "Wrong size, expected #{size_expected.to_s}, got #{size_actual.to_s}"
  end

  description do
    "Testing sizes"
  end
end

Spec::Matchers.define :be_at do |point_expected|
  match do |point_actual|
    (point_actual.x == point_expected.x) && (point_actual.y == point_expected.y)
  end

  failure_message_for_should do |point_actual|
    "Wrong point, expected #{point_expected.to_s}, got #{point_actual.to_s}"
  end

  description do
    "Testing points"
  end
end
